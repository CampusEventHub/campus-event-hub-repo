import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import RegisterModal from './RegisterModal';

function LoginModal({ show, handleClose }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [showRegisterModal, setShowRegisterModal] = useState(false);

  const handleLogin = () => {
    if (username === '' || password === '') {
      setError('Please fill out both fields.');
    } else {
      setError('');
      console.log('Login attempt:', { username, password });
      handleClose();
    }
  };

  const handleShowRegisterModal = () => {
    handleClose();
    setShowRegisterModal(true);
  };

  const handleCloseRegisterModal = () => {
    setShowRegisterModal(false);
  };

  return (
    <>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Login</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {error && <div className="text-danger">{error}</div>}
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
          <Button variant="link" onClick={handleShowRegisterModal}>
            Register an account
          </Button>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleLogin}>
            Login
          </Button>
        </Modal.Footer>
      </Modal>
      <RegisterModal
        show={showRegisterModal}
        handleClose={handleCloseRegisterModal}
      />
    </>
  );
}

export default LoginModal;