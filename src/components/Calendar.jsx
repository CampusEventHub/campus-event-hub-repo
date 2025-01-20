import React, { useState } from 'react';
import { Button, Row, Col } from 'react-bootstrap';
import DateModal from './DateModal';

const Calendar = ({ currentMonth, currentYear, setCurrentMonth, setCurrentYear }) => {
  const daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
  const [showModal, setShowModal] = useState(false);
  const [selectedDate, setSelectedDate] = useState(null);

  // Function to generate the calendar
  const generateCalendar = () => {
    const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay();
    const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate();
    const calendar = [];

    // Adjust for Monday as the first day of the week
    const adjustedFirstDay = firstDayOfMonth === 0 ? 6 : firstDayOfMonth - 1;

    let day = 1;
    for (let i = 0; i < 6; i++) {  // 6 rows for the calendar
      const week = [];
      for (let j = 0; j < 7; j++) {
        if (i === 0 && j < adjustedFirstDay) {
          week.push(null); // Empty spots before the first day of the month
        } else if (day > daysInMonth) {
          week.push(null); // Empty spots after the last day of the month
        } else {
          week.push(day);
          day++;
        }
      }
      calendar.push(week);
      if (day > daysInMonth) break;  // Stop if the day exceeds the number of days in the month
    }

    return calendar;
  };

  const handleDateClick = (date) => {
    setSelectedDate(date);
    setShowModal(true); // Open the modal when a date is clicked
  };

  const handleModalClose = () => setShowModal(false);
  const handleSave = (data) => {
    console.log('Saving data:', data);
    // You can save data to the backend or handle it as needed
  };

  // Handle month change (also handle year change when crossing boundaries)
  const handlePrevMonth = () => {
    if (currentMonth === 0) {
      setCurrentMonth(11); // December
      setCurrentYear(currentYear - 1);
    } else {
      setCurrentMonth(currentMonth - 1);
    }
  };

  const handleNextMonth = () => {
    if (currentMonth === 11) {
      setCurrentMonth(0); // January
      setCurrentYear(currentYear + 1);
    } else {
      setCurrentMonth(currentMonth + 1);
    }
  };

  // Render the calendar grid
  const calendar = generateCalendar();

  return (
    <div className="calendar">
      <Row className="calendar-header mb-3">
        <Col>
          <Button variant="outline-primary" onClick={handlePrevMonth}>&lt; Prev</Button>
        </Col>
        <Col className="text-center">
          <h3>{new Date(currentYear, currentMonth).toLocaleString('default', { month: 'long' })} {currentYear}</h3>
        </Col>
        <Col>
          <Button variant="outline-primary" onClick={handleNextMonth}>Next &gt;</Button>
        </Col>
      </Row>

      <div className="calendar-grid">
        <Row className="calendar-weekdays">
          {daysOfWeek.map((day) => (
            <Col key={day} className="text-center">
              <strong>{day}</strong>
            </Col>
          ))}
        </Row>

        {calendar.map((week, index) => (
          <Row key={index} className="calendar-week">
            {week.map((day, idx) => (
              <Col key={idx} className="calendar-day">
                {day ? <Button variant="outline-secondary" className="calendar-day-btn" onClick={() => handleDateClick(day)}>{day}</Button> : null}
              </Col>
            ))}
          </Row>
        ))}
      </div>

      <DateModal
        show={showModal}
        handleClose={handleModalClose}
        handleSave={handleSave}
        selectedDate={selectedDate}
      />
    </div>
  );
};

export default Calendar;