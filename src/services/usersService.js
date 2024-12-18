const API_URL = "https://localhost:7149/api/Users";

// Function to get user by ID
export const getUserById = async (userId) => {
  try {
    const response = await fetch(`${API_URL}/${userId}`);
    if (!response.ok) {
      throw new Error("Failed to fetch user data");
    }
    const user = await response.json();
    return user;
  } catch (error) {
    console.error("Error fetching user data:", error);
    throw error;
  }
};
