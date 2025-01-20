import React, { useState } from "react";
import { FaStar } from "react-icons/fa";
import "../App.css"; // Import the CSS file

const StarRating = ({ rating, onRatingChange }) => {
  const [hover, setHover] = useState(null);

  return (
    <div>
      {[...Array(5)].map((star, index) => {
        const ratingValue = index + 1;

        return (
          <label key={index} className="star-container">
            <input
              type="radio"
              name="rating"
              value={ratingValue}
              onClick={() => onRatingChange(ratingValue)}
              style={{ display: "none" }}
            />
            <FaStar
              size={30}
              color={ratingValue <= (hover || rating) ? "#ffc107" : "#a9a9a9"}
              onMouseEnter={() => setHover(ratingValue)}
              onMouseLeave={() => setHover(null)}
              className="star"
            />
          </label>
        );
      })}
    </div>
  );
};

export default StarRating;