import React, { useState, useEffect } from "react";
import { Modal, Button } from "react-bootstrap";
import { getUserById } from "../services/usersService";
import StarRating from "./StarRating";

const EventModal = ({ event, onClose }) => {
  const [userName, setUserName] = useState("");
  const [rating, setRating] = useState(0); // State to track the rating

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

  const handleRatingChange = (newRating) => {
    setRating(newRating);
    // Handle the rating change (e.g., save it to the backend later)
    console.log("Rating selected:", newRating);
  };

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

        {/* Star Rating */}
        <div style={{ marginTop: "20px" }}>
          <h5>Rate this event:</h5>
          <StarRating rating={rating} onRatingChange={handleRatingChange} />
        </div>
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