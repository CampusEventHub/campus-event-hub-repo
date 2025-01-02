import React from 'react';
import { Modal, Button } from 'react-bootstrap';

function WelcomeModal({ show, handleClose }) {
  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Welcome to Campus Event</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h5>Welcome! You have successfully logged in.</h5>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="primary" onClick={handleClose}>
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default WelcomeModal;