import React, { useState } from 'react';
import { ListGroup, ListGroupItem, Row, Col, Image } from 'react-bootstrap';
import EventModal from './EventModal';
import '../App.css';

function EventList({ events }) {
  const [selectedEvent, setSelectedEvent] = useState(null);

  const handleEventClick = (event) => {
    setSelectedEvent(event);  // kliknuti event je selectan
  };

  const handleCloseModal = () => {
    setSelectedEvent(null);  // resetira selectani event da bi zatvorili
  };

  return (
    <div className="event-list-container">
      <ListGroup className="scrollable-list">
        {events.map(event => (
          <ListGroupItem
            key={event.id}
            className="d-flex align-items-center event-container"
            onClick={() => handleEventClick(event)}
            style={{ cursor: 'pointer' }}
          >
            <Row className="w-100">
              <Col xs={3} md={2}>
                <Image
                  src={event.imageUrl || 'https://via.placeholder.com/150'}
                  rounded
                  fluid
                />
              </Col>
              <Col xs={9} md={10}>
                <h4><b>{event.title}</b></h4>
                <p>{event.description}</p>
                <br />
                <br />
                <p className="event-start-date"><b>Start Date:</b> {new Date(event.startDate).toLocaleString()}</p>
              </Col>
            </Row>
          </ListGroupItem>
        ))}
      </ListGroup>
      {selectedEvent && (
        <EventModal event={selectedEvent} onClose={handleCloseModal} />
      )}
    </div>
  );
  
}

export default EventList;
