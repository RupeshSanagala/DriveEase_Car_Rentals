using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.Converters;
using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;
//using Car_Rental_Backend_Application.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly CarRentalContext _context;
    private readonly EmailService _emailService;

    public BookingController(CarRentalContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    //[HttpPost]
    //public async Task<ActionResult<BookingResponseDto>> CreateBooking(BookingRequestDto bookingRequestDto)
    //{
    //    if (bookingRequestDto == null)
    //        return BadRequest("Booking data is required.");

    //    try
    //    {
    //        using var transaction = await _context.Database.BeginTransactionAsync();

    //        var user = await _context.Users
    //            .Include(u => u.Bookings)
    //            .FirstOrDefaultAsync(u => u.UserId == bookingRequestDto.User_ID);

    //        if (user == null)
    //            return NotFound("User not found.");

    //        var car = await _context.Cars
    //            .Include(c => c.Bookings)
    //            .FirstOrDefaultAsync(c => c.Car_ID == bookingRequestDto.Car_ID);

    //        if (car == null)
    //            return NotFound("Car not found.");

    //        if (car.Availability_Status == "Booked")
    //            return BadRequest("Car is currently rented and not available for booking.");

    //        bool isCarBooked = await _context.Bookings.AnyAsync(b =>
    //            b.Car_ID == bookingRequestDto.Car_ID &&
    //            (b.PickupDate <= bookingRequestDto.ReturnDate && b.ReturnDate >= bookingRequestDto.PickupDate));

    //        if (isCarBooked)
    //            return BadRequest("Car is already booked for the selected dates.");

    //        if (bookingRequestDto.PickupDate >= bookingRequestDto.ReturnDate)
    //            return BadRequest("Invalid booking dates.");

    //        var totalDays = (bookingRequestDto.ReturnDate - bookingRequestDto.PickupDate).Days;
    //        var totalPrice = totalDays * car.PricePerDay;

    //        var booking = new Booking
    //        {
    //            User_ID = bookingRequestDto.User_ID,
    //            Car_ID = bookingRequestDto.Car_ID,
    //            BookingDate = bookingRequestDto.BookingDate,
    //            PickupDate = bookingRequestDto.PickupDate,
    //            ReturnDate = bookingRequestDto.ReturnDate,
    //            TotalPrice = totalPrice,
    //            User = user,
    //            Car = car
    //        };

    //        _context.Bookings.Add(booking);
    //        await _context.SaveChangesAsync();

    //        user.Bookings.Add(booking);
    //        _context.Users.Update(user);

    //        car.Bookings.Add(booking);
    //        car.Availability_Status = "Booked";
    //        _context.Cars.Update(car);

    //        await _context.SaveChangesAsync();
    //        await transaction.CommitAsync();

    //        // ✅ Send Booking Confirmation Email
    //        string emailSubject = "Booking Confirmation - Car Rental";
    //        string emailBody = $@"
    //    <h2>Dear {user.Username},</h2>
    //    <p>Your booking has been successfully confirmed.</p>
    //    <h3>Booking Details:</h3>
    //    <ul>
    //        <li><strong>Booking ID:</strong> {booking.BookingId}</li>
    //        <li><strong>Car Model:</strong> {car.Model}</li>
    //        <li><strong>Pickup Date:</strong> {booking.PickupDate:yyyy-MM-dd}</li>
    //        <li><strong>Return Date:</strong> {booking.ReturnDate:yyyy-MM-dd}</li>
    //        <li><strong>Total Price:</strong> ${booking.TotalPrice}</li>
    //    </ul>
    //    <p>Thank you for choosing our service!</p>
    //    <p>Best Regards, <br/>Car Rental Team</p>";

    //        await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);

    //        return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId },
    //            BookingConverters.BookingToBookingResponseDto(booking));
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"Internal server error: {ex.Message}");
    //    }
    //}

    [HttpPost("create/{userId}")]
    public async Task<ActionResult<CarBookingResponseDto>> CreateBooking(int userId, [FromBody] CarBookingRequestDto bookingRequest)
    {
        if (bookingRequest == null)
            return BadRequest("Booking request data is required.");

        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            // 🔹 Fetch User
            var user = await _context.Users
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return NotFound("User not found.");

            // 🔹 Fetch Car
            var car = await _context.Cars
                .Include(c => c.Bookings)
                .FirstOrDefaultAsync(c => c.Car_ID == bookingRequest.Car_ID);

            if (car == null)
                return NotFound("Car not found.");

            if (car.Availability_Status == "Booked")
                return BadRequest("Car is currently rented and not available for booking.");

            // 🔹 Convert string dates to DateTime for comparison and calculation
            DateTime pickupDate = DateTime.ParseExact(bookingRequest.PickupDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime returnDate = DateTime.ParseExact(bookingRequest.ReturnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            // 🔹 Check if car is already booked in the given date range
            bool isCarBooked = await _context.Bookings.AnyAsync(b =>
                b.Car_ID == bookingRequest.Car_ID &&
                (DateTime.ParseExact(b.PickupDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) <= returnDate &&
                 DateTime.ParseExact(b.ReturnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= pickupDate));

            if (isCarBooked)
                return BadRequest("Car is already booked for the selected dates.");

            if (pickupDate >= returnDate)
                return BadRequest("Invalid booking dates.");

            // 🔹 Calculate total price
            var totalDays = (returnDate - pickupDate).Days;
            var totalPrice = totalDays * car.PricePerDay;

            // 🔹 Create new booking entity
            var booking = new Booking
            {
                User_ID = userId,
                Car_ID = bookingRequest.Car_ID,
                BookingDate = DateTime.Now.ToString("dd-MM-yyyy"), // Store as string in the "dd-MM-yyyy" format
                PickupDate = bookingRequest.PickupDate,  // Store as string
                ReturnDate = bookingRequest.ReturnDate,  // Store as string
                TotalPrice = totalPrice,
                User = user,
                Car = car
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            user.Bookings.Add(booking);
            _context.Users.Update(user);

            car.Bookings.Add(booking);
            car.Availability_Status = "Booked";
            _context.Cars.Update(car);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // ✅ Send Booking Confirmation Email
            string emailSubject = "Booking Confirmation - Car Rental";
            string emailBody = $@"
            <h2>Dear {user.Username},</h2>
            <p>Your booking has been successfully confirmed.</p>
            <h3>Booking Details:</h3>
            <ul>
                <li><strong>Booking ID:</strong> {booking.BookingId}</li>
                <li><strong>Car Model:</strong> {car.Model}</li>
                <li><strong>Pickup Date:</strong> {pickupDate:yyyy-MM-dd}</li>
                <li><strong>Return Date:</strong> {returnDate:yyyy-MM-dd}</li>
                <li><strong>Total Price:</strong> ${booking.TotalPrice}</li>
            </ul>
            <p>Thank you for choosing our service!</p>
            <p>Best Regards, <br/>Car Rental Team</p>";

            await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);

            // ✅ Convert and return response DTO
            var bookingResponse = new CarBookingResponseDto
            {
                User_ID = user.UserId,
                UserName = user.Username,
                Email = user.Email,
                Car_ID = car.Car_ID,
                CarDetails = $"{car.Brand} {car.Model}",
                BookingDate = booking.BookingDate,
                PickupDate = pickupDate.ToString("dd-MM-yyyy"),  // Format as string for response
                ReturnDate = returnDate.ToString("dd-MM-yyyy"),  // Format as string for response
                TotalPrice = totalPrice
            };

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, bookingResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }




    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetAllBookings()
    {
        var bookings = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Car)
            .ToListAsync();

        if (!bookings.Any())
            return NotFound("No bookings found.");

        return Ok(bookings.Select(BookingConverters.BookingToBookingResponseDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingResponseDto>> GetBookingById(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.BookingId == id);

        if (booking == null)
            return NotFound($"Booking with ID {id} not found.");

        return Ok(BookingConverters.BookingToBookingResponseDto(booking));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, BookingRequestDto bookingRequestDto)
    {
        var booking = await _context.Bookings
            .Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.BookingId == id);

        if (booking == null)
            return NotFound($"Booking with ID {id} not found.");

        // 🔹 Convert string dates to DateTime for comparison and calculation
        DateTime pickupDate = DateTime.ParseExact(bookingRequestDto.PickupDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        DateTime returnDate = DateTime.ParseExact(bookingRequestDto.ReturnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        // 🔹 Validate Booking Dates
        if (pickupDate >= returnDate)
            return BadRequest("Invalid booking dates.");

        // 🔹 Check for overlapping bookings
        bool isCarBooked = await _context.Bookings.AnyAsync(b =>
            b.Car_ID == booking.Car_ID &&
            b.BookingId != id && // Exclude current booking
            (DateTime.ParseExact(b.PickupDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) <= returnDate &&
             DateTime.ParseExact(b.ReturnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= pickupDate)
        );

        if (isCarBooked)
            return BadRequest("Car is already booked for the selected dates.");

        // 🔹 Recalculate Total Price
        var totalDays = (returnDate - pickupDate).Days;
        if (totalDays <= 0)
            return BadRequest("Booking duration must be at least one day.");

        var totalPrice = totalDays * booking.Car.PricePerDay;

        // 🔹 Update Booking Details
        booking.PickupDate = bookingRequestDto.PickupDate;  // Store as string
        booking.ReturnDate = bookingRequestDto.ReturnDate;  // Store as string
        booking.TotalPrice = totalPrice;

        try
        {
            await _context.SaveChangesAsync();
            return Ok($"Booking {id} updated successfully.");
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, "Database concurrency issue. Please try again.");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            return NotFound($"Booking with ID {id} not found.");

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookingsByUserId(int userId)
    {
        var user = await _context.Users
            .Include(u => u.Bookings)
            .ThenInclude(b => b.Car) // ✅ Include Car details
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
            return NotFound($"User with ID {userId} not found.");

        if (!user.Bookings.Any())
            return NotFound($"No bookings found for User ID {userId}.");

        // ✅ Convert Bookings to Response DTOs
        var bookingsDto = user.Bookings.Select(BookingConverters.BookingToBookingResponseDto);

        return Ok(bookingsDto);
    }

}