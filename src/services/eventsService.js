import axios from 'axios';

const API_URL = 'https://localhost:7149/api/Events';


export const fetchEvents = async () => {
  try {
    const token = localStorage.getItem('authToken');
    const response = await axios.get(API_URL, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching events:", error);
    throw error;
  }
};
