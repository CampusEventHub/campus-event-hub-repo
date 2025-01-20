import React, { useState } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';

const DateModal = ({ show, handleClose, handleSave, selectedDate }) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [time, setTime] = useState('');

  const handleSubmit = () => {
    const data = {
      date: selectedDate, 
      title,
      description,
      time
    };
    handleSave(data);
    handleClose();  // Close the modal after saving
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Set Reminder for {selectedDate}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="formTitle">
            <Form.Label>Title</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter title"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
            />
          </Form.Group>

          <Form.Group controlId="formDescription">
            <Form.Label>Description</Form.Label>
            <Form.Control
              as="textarea"
              rows={3}
              placeholder="Enter description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
          </Form.Group>

          <Form.Group controlId="formTime">
            <Form.Label>Time</Form.Label>
            <Form.Control
              type="time"
              value={time}
              onChange={(e) => setTime(e.target.value)}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={handleSubmit}>
          Save
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default DateModal;