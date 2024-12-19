import React, { useState, useEffect } from "react";
import { Modal, Button } from "react-bootstrap";
import { getUserById } from "../services/usersService";

const EventModal = ({ event, onClose }) => {
  const [userName, setUserName] = useState(""); 


  useEffect(() => {
    const fetchUserName = async () => {
      if (event && event.userID) {
        try {
          const user = await getUserById(event.userID);
          setUserName(user.username);
        } catch (error) {
          console.error("Failed to fetch user data:", error);
        }
      }
    };

    fetchUserName();
  }, [event]);

  if (!event) return null;

  return (
    <Modal show={true} onHide={onClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>{event.title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <img
          src={event.imageUrl}
          alt={event.title}
          style={{ width: "100%", height: "auto", marginBottom: "10px" }}
        />
        <p>{event.description}</p>
        <p>
          <strong>Location:</strong> {event.location}
        </p>
        <p>
          <strong>Start:</strong> {new Date(event.startDate).toLocaleString()}
        </p>
        <p>
          <strong>End:</strong> {new Date(event.endDate).toLocaleString()}
        </p>
        <p>
          <strong>Created By:</strong> {userName || "Loading..."}
        </p>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onClose}>
          Close
        </Button>
        <Button variant="primary" onClick={() => alert("Joined!")}>
          Join
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default EventModal;
