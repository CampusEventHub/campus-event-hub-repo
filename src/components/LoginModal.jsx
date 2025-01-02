import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { loginUser } from '../services/usersService';

function LoginModal({ show, handleClose, handleShowRegisterModal, handleShowWelcomeModal }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleLogin = async () => {
    setError('');
    setSuccess('');
  
    if (username === '' || password === '') {
      setError('Please fill out both fields.');
      return;
    }
  
    try {
      const token = await loginUser(username, password);
      console.log('Token received:', token); // Log the token for debugging
  
      if (token) {
        localStorage.setItem('authToken', token);
        setSuccess('Login successful!');
        
        // Close Login Modal and open Welcome Modal
        setTimeout(() => {
          handleClose();            // Close Login Modal
          handleShowWelcomeModal(); // Open Welcome Modal
        }, 1000); // Add a 1-second delay before closing
      }
    } catch (err) {
      setError('Invalid credentials or something went wrong. Please try again.');
    }
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Login</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {error && <div className="text-danger">{error}</div>}
        {success && <div className="text-success">{success}</div>}
        <Form>
          <Form.Group className="mb-3" controlId="formUsername">
            <Form.Label>Username</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formPassword">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={handleLogin}>
          Login
        </Button>
      </Modal.Footer>
      {/* Register button */}
      <div className="text-center">
        <Button variant="link" onClick={() => { handleClose(); handleShowRegisterModal(); }}>
          Don't have an account? Register here
        </Button>
      </div>
    </Modal>
  );
}

export default LoginModal;