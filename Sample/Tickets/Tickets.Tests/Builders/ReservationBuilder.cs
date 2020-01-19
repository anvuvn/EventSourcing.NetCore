using System;
using Core.Aggregates;
using Tickets.Reservations;
using Tickets.Tests.Stubs.Ids;
using Tickets.Tests.Stubs.Reservations;

namespace Tickets.Tests.Builders
{
    internal class ReservationBuilder
    {
        private Func<Reservation> build  = () => null;

        public ReservationBuilder Tentative()
        {
            var idGenerator = new FakeAggregateIdGenerator<Reservation>();
            var numberGenerator = new FakeReservationNumberGenerator();
            var seatId = Guid.NewGuid();

            // When
            var reservation = Reservation.CreateTentative(
                idGenerator,
                numberGenerator,
                seatId
            );

            build = () => reservation;

            return this;
        }

        public static ReservationBuilder Create() => new ReservationBuilder();

        public Reservation Build()
        {
            var reservation = build();
            ((IAggregate)reservation).DequeueUncommittedEvents();
            return reservation;
        }
    }
}