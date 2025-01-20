import React from 'react';

const CalendarCard = ({ event }) => {
  const startDate = new Date(event.startDate);

  const formattedDate = startDate.toLocaleString('en-GB', {
    weekday: 'short', 
    year: 'numeric',
    month: 'numeric',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    hour12: false,
  });

  return (
    <div className="calendar-card">
      <h3><strong>{event.naslov}</strong></h3>
      <p>{event.opis}</p>
      <p><strong>Start Date:</strong> <span className="event-date-time">{formattedDate}</span></p>
    </div>
  );
};

export default CalendarCard;