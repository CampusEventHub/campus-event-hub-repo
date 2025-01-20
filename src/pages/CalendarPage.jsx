import React, { useEffect, useState } from 'react';
import Calendar from '../components/Calendar';
import { fetchCalendarData } from '../services/calanderService';
import CalendarCard from '../components/CalendarCard';

const CalendarPage = () => {
  const [events, setEvents] = useState([]);
  const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
  const [currentYear, setCurrentYear] = useState(new Date().getFullYear());

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const data = await fetchCalendarData(currentMonth + 1, currentYear);
        console.log('Event data:', data);

        // Sort events by date and time
        const sortedEvents = data.sort((a, b) => {
          const dateA = new Date(a.startDate);
          const dateB = new Date(b.startDate);

          // First, compare by date
          if (dateA.getTime() !== dateB.getTime()) {
            return dateA - dateB;
          }

          // If dates are equal, compare by time (hour and minute)
          const timeA = dateA.getHours() * 60 + dateA.getMinutes();
          const timeB = dateB.getHours() * 60 + dateB.getMinutes();

          return timeA - timeB;
        });

        setEvents(sortedEvents);
      } catch (error) {
        console.error('Error fetching calendar events:', error);
      }
    };

    fetchEvents();
  }, [currentMonth, currentYear]);

  return (
    <div className="calendar-page">
      <h1>Event Calendar</h1>
      <Calendar
        currentMonth={currentMonth}
        currentYear={currentYear}
        setCurrentMonth={setCurrentMonth}
        setCurrentYear={setCurrentYear}
      />
      <div className="calendar-cards-container">
        {events.length === 0 ? (
          <p>No events available for this month.</p>
        ) : (
          events.map((event) => (
            <CalendarCard key={event.id} event={event} />
          ))
        )}
      </div>
    </div>
  );
};

export default CalendarPage;