const API_URL = "https://localhost:7149/api/Users";

// geta usera po id-u
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
