import React from 'react';
import { Navbar as BootstrapNavbar, Nav, Form, Button, FormControl, Container } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { FaUserCircle } from 'react-icons/fa';
import '../App.css';

function NavBar({ onProfileClick }) {
  return (
    <BootstrapNavbar bg="dark" variant="dark" expand="lg">
      <Container>
        <BootstrapNavbar.Brand as={Link} to="/">Campus Event Hub</BootstrapNavbar.Brand>
        <BootstrapNavbar.Toggle aria-controls="navbar-nav" />
        <BootstrapNavbar.Collapse id="navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/">Home</Nav.Link>
            <Nav.Link as={Link} to="/calendar">Calendar</Nav.Link> {/* Add this link */}
          </Nav>
          <Form className="d-flex">
            <FormControl
              type="text"
              placeholder="Search Events"
              className="me-2 search-input"
            />
            <Button variant="outline-success" disabled>
              Search
            </Button>
          </Form>
          <Nav>
            <Nav.Link onClick={onProfileClick}>
              <FaUserCircle size={30} color="white" />
            </Nav.Link>
          </Nav>
        </BootstrapNavbar.Collapse>
      </Container>
    </BootstrapNavbar>
  );
}

export default NavBar;
