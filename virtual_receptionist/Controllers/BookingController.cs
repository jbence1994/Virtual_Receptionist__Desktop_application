﻿using System;
using System.Data;
using System.Collections.Generic;
using virtual_receptionist.Controllers.Exceptions;
using virtual_receptionist.Controllers.Validation;
using virtual_receptionist.Models.Data;
using virtual_receptionist.Models.ORM;

namespace virtual_receptionist.Controllers
{
    /// <summary>
    /// Foglalási napló modul vezérlője
    /// </summary>
    public class BookingController : Controller
    {
        #region Adattagok

        /// <summary>
        /// Foglalások adattár osztály egy példánya
        /// </summary>
        private BookingRepository repository;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Foglalási napló modul vezérlő konstruktora
        /// </summary>
        public BookingController()
        {
            repository = new BookingRepository();
        }

        #endregion

        #region Metódusok

        /// <summary>
        /// Metódus, amely lekéri a foglalások adatait érkezés dátuma alapján az adattárból és visszaadja őket egy adattáblában
        /// </summary>
        /// <param name="arrivalDate">Érkezés dátuma</param>
        /// <returns>A céges foglalások adataival - érkezés dátuma szerint - feltöltött adattáblát adja vissza a függvény</returns>
        public DataTable GetBookingsByArrivalDate(DateTime arrivalDate)
        {
            string arrival = arrivalDate.ToString("yyyy-MM-dd");
            List<Booking> bookingsByArrival = repository.GetGuestBookingsByArrivalDate(arrival);

            DataTable bookingsDataTableByArrival = new DataTable();
            bookingsDataTableByArrival.Columns.Add("ID", typeof(int));
            bookingsDataTableByArrival.Columns.Add("GuestName", typeof(string));
            bookingsDataTableByArrival.Columns.Add("RoomNumber", typeof(int));
            bookingsDataTableByArrival.Columns.Add("NumberOfGuests", typeof(int));
            bookingsDataTableByArrival.Columns.Add("ArrivalDate", typeof(string));
            bookingsDataTableByArrival.Columns.Add("DepartureDate", typeof(string));
            bookingsDataTableByArrival.Columns.Add("Paid", typeof(string));

            string paid = "Nem";

            foreach (Booking booking in bookingsByArrival)
            {
                if (booking.Paid)
                {
                    paid = "Igen";
                }

                bookingsDataTableByArrival.Rows.Add(booking.ID, booking.Guest.Name, booking.Room.Number,
                    booking.NumberOfGuests, booking.ArrivalDate, booking.DepartureDate, paid);
            }

            return bookingsDataTableByArrival;
        }

        /// <summary>
        /// Metódus, amely lekéri a foglalások adatait távozás dátuma alapján az adattárból és visszaadja őket egy adattáblában
        /// </summary>
        /// <param name="departureDate">Távozás dátuma</param>
        /// <returns>A foglalások adataival - távozás dátuma szerint - feltöltött adattáblát adja vissza a függvény</returns>
        public DataTable GetBookingsByDepartureDate(DateTime departureDate)
        {
            string departure = departureDate.ToString("yyyy-MM-dd");
            List<Booking> bookingsByDeparture = repository.GetGuestBookingsByDepartureDate(departure);

            DataTable bookingsDataTableByDeparture = new DataTable();
            bookingsDataTableByDeparture.Columns.Add("ID", typeof(int));
            bookingsDataTableByDeparture.Columns.Add("GuestName", typeof(string));
            bookingsDataTableByDeparture.Columns.Add("RoomNumber", typeof(int));
            bookingsDataTableByDeparture.Columns.Add("NumberOfGuests", typeof(int));
            bookingsDataTableByDeparture.Columns.Add("ArrivalDate", typeof(string));
            bookingsDataTableByDeparture.Columns.Add("DepartureDate", typeof(string));
            bookingsDataTableByDeparture.Columns.Add("Paid", typeof(string));

            string paid = "Nem";

            foreach (Booking booking in bookingsByDeparture)
            {
                if (booking.Paid)
                {
                    paid = "Igen";
                }

                bookingsDataTableByDeparture.Rows.Add(booking.ID, booking.Guest.Name, booking.Room.Number,
                    booking.NumberOfGuests, booking.ArrivalDate, booking.DepartureDate, paid);
            }

            return bookingsDataTableByDeparture;
        }

        /// <summary>
        /// Új foglalás felvétele
        /// </summary>
        /// <param name="bookingParameters">Foglalás paraméterei</param>
        public void AddBooking(params object[] bookingParameters)
        {
            Guest guest = new Guest()
            {
                Name = bookingParameters[1].ToString()
            };

            Room room = new Room()
            {
                Number = Convert.ToInt32(bookingParameters[3])
            };

            int numberOfGuests = Convert.ToInt32(bookingParameters[4]);

            string arrivalDate = Convert.ToDateTime(bookingParameters[5]).ToString("yyyy-MM-dd");

            string departureDate = Convert.ToDateTime(bookingParameters[6]).ToString("yyyy-MM-dd");

            bool paid = Convert.ToBoolean(bookingParameters[7]);

            Booking booking = new Booking(guest, room, numberOfGuests, arrivalDate, departureDate, paid);

            repository.Create(booking);
        }

        /// <summary>
        /// Foglalás törlése
        /// </summary>
        /// <param name="bookingParameters">Foglalás paraméterei</param>
        public void DeleteBooking(params object[] bookingParameters)
        {
            int id = Convert.ToInt32(bookingParameters[0]);

            Guest guest = new Guest()
            {
                Name = bookingParameters[1].ToString()
            };

            Room room = new Room()
            {
                Number = Convert.ToInt32(bookingParameters[3])
            };

            int numberOfGuests = Convert.ToInt32(bookingParameters[4]);

            string arrivalDate = Convert.ToDateTime(bookingParameters[5].ToString()).ToString("yyyy-MM-dd");

            string departureDate = Convert.ToDateTime(bookingParameters[6].ToString()).ToString("yyyy-MM-dd");

            bool paid = Convert.ToBoolean(bookingParameters[7]);

            Booking booking = new Booking(id, guest, room, numberOfGuests, arrivalDate, departureDate, paid);

            repository.Delete(booking);
        }

        /// <summary>
        /// Foglalás módosítása
        /// </summary>
        /// <param name="bookingParameters">Foglalás paraméterei</param>
        public void UpdateBooking(params object[] bookingParameters)
        {
            int id = Convert.ToInt32(bookingParameters[0]);

            Guest guest = new Guest()
            {
                Name = bookingParameters[1].ToString()
            };

            Room room = new Room()
            {
                Number = Convert.ToInt32(bookingParameters[3])
            };

            int numberOfGuests = Convert.ToInt32(bookingParameters[4]);

            string arrivalDate = Convert.ToDateTime(bookingParameters[5]).ToString("yyyy-MM-dd");

            string departureDate = Convert.ToDateTime(bookingParameters[6]).ToString("yyyy-MM-dd");

            bool paid = Convert.ToBoolean(bookingParameters[7]);

            Booking booking = new Booking(id, guest, room, numberOfGuests, arrivalDate, departureDate, paid);

            repository.Update(booking);
        }

        /// <summary>
        /// Foglalás dátumát ellenőrző metódus
        /// </summary>
        /// <param name="arrivalDate">Érkezés dátuma</param>
        /// <param name="departureDate">Távozás dátuma</param>
        ///<exception cref="InvalidBookingParameterException"></exception>
        public void BookingDateValidator(DateTime arrivalDate, DateTime departureDate)
        {
            Booking booking = new Booking()
            {
                ArrivalDate = arrivalDate.ToString(),
                DepartureDate = departureDate.ToString()
            };

            try
            {
                BookingDateValidation bookingDateValidation = new BookingDateValidation(booking);
                bookingDateValidation.ValidateBookingDate();
            }
            catch (InvalidBookingParameterException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Foglalás kapacitását ellenőrző metódus
        /// </summary>
        /// <param name="numberOfGuests">Vendégek száma</param>
        /// <param name="roomNumber">Szobaszám</param>
        /// <exception cref="InvalidBookingParameterException"></exception>
        public void BookingCapacityValidator(decimal numberOfGuests, int roomNumber)
        {
            Room room = new Room()
            {
                Capacity = repository.GetRoomCapacity(roomNumber)
            };

            Booking booking = new Booking()
            {
                NumberOfGuests = Convert.ToInt32(numberOfGuests),
                Room = room
            };

            try
            {
                BookingCapacityValidation bookingCapacityValidation = new BookingCapacityValidation(booking, room);
                bookingCapacityValidation.ValidateBookingCapacity();
            }
            catch (InvalidBookingParameterException e)
            {
                throw e;
            }
        }

        #endregion
    }
}
