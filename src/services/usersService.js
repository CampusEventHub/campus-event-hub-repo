export const loginUser = async (username, password) => {
  try {
    const response = await fetch('https://localhost:7149/api/Auth/Login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ username, password }),  // Sending username and password
    });

    if (!response.ok) {
      throw new Error('Failed to log in');
    }

    const data = await response.json();
    console.log('Login response:', data);  // Log the response to check if token is returned

    return data.token;  // Assuming the backend returns a token
  } catch (error) {
    console.error('Login error:', error);
    throw error;
  }
};

export const getUserById = async (userId) => {
  try {
    // Get the token from localStorage
    const token = localStorage.getItem('authToken');
    
    if (!token) {
      throw new Error('No auth token found');
    }

    const response = await fetch(`https://localhost:7149/api/users/${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`, // Use stored JWT token
      },
    });

    if (!response.ok) {
      throw new Error('Failed to fetch user details');
    }

    const userData = await response.json();
    return userData; // Assuming user data is returned in the response
  } catch (error) {
    console.error('Get user by ID error:', error);
    throw error;
  }
};