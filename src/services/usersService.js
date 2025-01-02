export const loginUser = async (username, password) => {
  try {
    const response = await fetch('https://localhost:7149/api/Auth/Login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
      throw new Error('Failed to log in');
    }

    const data = await response.json();
    console.log('Login response:', data);

    return data.token;
  } catch (error) {
    console.error('Login error:', error);
    throw error;
  }
};

export const getUserById = async (userId) => {
  try {
    const token = localStorage.getItem('authToken');
    
    if (!token) {
      throw new Error('No auth token found');
    }

    const response = await fetch(`https://localhost:7149/api/users/${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (!response.ok) {
      throw new Error('Failed to fetch user details');
    }

    const userData = await response.json();
    return userData;
  } catch (error) {
    console.error('Get user by ID error:', error);
    throw error;
  }
};