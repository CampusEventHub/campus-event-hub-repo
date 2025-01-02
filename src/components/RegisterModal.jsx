import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';

function RegisterModal({ show, handleClose }) {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleRegister = () => {
    // provjertava jeli prazno ista
    if (username === '' || email === '' || password === '' || confirmPassword === '') {
      setError('Please fill out all fields.');
      setSuccess('');
      return;
    }

    // jese podudaraju sifre
    if (password !== confirmPassword) {
      setError('Passwords do not match.');
      setSuccess('');
      return;
    }

    // sifra mora bit duza od 6 znaka
    if (password.length < 6) {
      setError('Password must be at least 6 characters long.');
      setSuccess('');
      return;
    }

    // USPILO!!!!!!! u zelenoj boji B-)
    setError('');
    setSuccess('Registration successful!');
    console.log('Registration attempt:', { username, email, password });

    //2 sekunde delay da ne zatvori pre brzo
    setTimeout(() => {
        handleClose();
      }, 2000);


      console.log('Registration attempt:', { username, email, password });
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