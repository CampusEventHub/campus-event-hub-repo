import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import axios from 'axios';

function RegisterModal({ show, handleClose }) {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleRegister = async () => {

    if (username === '' || email === '' || password === '' || confirmPassword === '') {
      setError('Please fill out all fields.');
      setSuccess('');
      return;
    }

    if (password !== confirmPassword) {
      setError('Passwords do not match.');
      setSuccess('');
      return;
    }

    if (password.length < 6) {
      setError('Password must be at least 6 characters long.');
      setSuccess('');
      return;
    }

    setError('');
    setSuccess('');

    const userData = {
      username: username,
      email: email,
      password: password,
    };

    try {
      const response = await axios.post('https://localhost:7149/api/Auth/Register', userData);

      if (response.status === 200) {
        setSuccess('Registration successful!');
        setTimeout(() => {
          handleClose();
        }, 2000);
      }
    } catch (err) {
      if (err.response && err.response.data) {
        setError(err.response.data);
      } else {
        setError('Something went wrong. Please try again later.');
      }
    }
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Register</Modal.Title>
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
          <Form.Group className="mb-3" controlId="formEmail">
            <Form.Label>Email</Form.Label>
            <Form.Control
              type="email"
              placeholder="Enter email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
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
          <Form.Group className="mb-3" controlId="formConfirmPassword">
            <Form.Label>Confirm Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Confirm Password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={handleRegister}>
          Register
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default RegisterModal;