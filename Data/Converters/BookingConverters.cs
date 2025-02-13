﻿using Car_Rental_Backend_Application.Controllers;
using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace Car_Rental_Backend_Application.Data.Converters
{
    public static class BookingConverters
    {
        private static readonly CarRentalContext _context;
        public static BookingResponseDto BookingToBookingResponseDto(Booking booking)
        {
            
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            return new BookingResponseDto
            {
                id = booking.BookingId,
                User_ID = booking.User_ID,
                UserName = booking.User?.Username,
                Email = booking.User?.Email,
                Car_ID = booking.Car_ID,
                CarDetails = $"{booking.Car?.Brand} {booking.Car?.Model}",
                BookingDate = booking.BookingDate,
                PickupDate = booking.PickupDate,
                ReturnDate = booking.ReturnDate,
                TotalPrice = booking.TotalPrice,
                //CancellationIds = booking.Cancellations?.Select(c => c.Cancellation_ID).ToList() ?? new List<int>()
            };
        }

        public static async Task<CarBookingResponseDto> CarBookingRequestDtoToCarBookingResponseDto(
      CarBookingRequestDto carBookingRequestDto)
        {
            if (carBookingRequestDto == null)
                throw new ArgumentNullException(nameof(carBookingRequestDto));

            // Retrieve Car details using the provided Car_ID
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Car_ID == carBookingRequestDto.Car_ID);

            if (car == null)
                throw new InvalidOperationException($"Car with ID {carBookingRequestDto.Car_ID} not found.");

            // Convert PickupDate and ReturnDate from string to DateTime
            DateTime pickupDate = DateTime.ParseExact(carBookingRequestDto.PickupDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime returnDate = DateTime.ParseExact(carBookingRequestDto.ReturnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            // Calculate total price using DateTime values
            decimal totalPrice = CalculateTotalPrice(pickupDate, returnDate, car.PricePerDay);

            // Create and return the CarBookingResponseDto
            return new CarBookingResponseDto
            {
                Car_ID = car.Car_ID,
                CarDetails = $"{car.Brand} {car.Model}",
                PickupDate = pickupDate.ToString("dd-MM-yyyy"), // Convert DateTime back to string in desired format
                ReturnDate = returnDate.ToString("dd-MM-yyyy"), // Convert DateTime back to string in desired format
                TotalPrice = totalPrice
            };
        }

        // Helper method to calculate total price for the booking
        private static decimal CalculateTotalPrice(DateTime pickupDate, DateTime returnDate, decimal pricePerDay)
        {
            var totalDays = (returnDate - pickupDate).Days; // Calculate days difference
            return totalDays * pricePerDay; // Multiply by price per day
        }


        // Helper method to calculate total price for the booking
        //private static decimal CalculateTotalPrice(DateTime pickupDate, DateTime returnDate, decimal pricePerDay)
        //{
        //    var totalDays = (returnDate - pickupDate).Days;
        //    return totalDays * pricePerDay;
        //}
        public static Booking BookingRequestDtoToBooking(BookingRequestDto bookingRequestDto)
        {
            if (bookingRequestDto == null)
                throw new ArgumentNullException(nameof(bookingRequestDto));

            return new Booking
            {
                User_ID = bookingRequestDto.User_ID,
                Car_ID = bookingRequestDto.Car_ID,
                BookingDate = bookingRequestDto.BookingDate,
                PickupDate = bookingRequestDto.PickupDate,
                ReturnDate = bookingRequestDto.ReturnDate,
             
            };
        }
    }
}
