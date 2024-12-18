import axios from 'axios';

const API_URL = 'https://localhost:7149/api/Events';

export const fetchEvents = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching events:", error);
    throw error; //nazad komponenti throwan error
  }
};
