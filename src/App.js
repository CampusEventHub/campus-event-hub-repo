import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar'; // cacheirano staro ime mozda ali radi?? xd
import LoginModal from './components/LoginModal';
import RegisterModal from './components/RegisterModal';
import WelcomeModal from './components/WelcomeModal';
import HomePage from './pages/HomePage';
import CalendarPage from './pages/CalendarPage'; // Import the new CalendarPage component

function App() {
  const [showLogin, setShowLogin] = useState(false);
  const [showRegister, setShowRegister] = useState(false);
  const [showWelcome, setShowWelcome] = useState(false);

  const handleShowLogin = () => setShowLogin(true);
  const handleCloseLogin = () => setShowLogin(false);
  
  const handleShowRegisterModal = () => setShowRegister(true);
  const handleCloseRegister = () => setShowRegister(false);
  
  const handleShowWelcomeModal = () => setShowWelcome(true);
  const handleCloseWelcomeModal = () => setShowWelcome(false);

  return (
    <Router>
      <NavBar onProfileClick={handleShowLogin} />
      
      <LoginModal
        show={showLogin}
        handleClose={handleCloseLogin}
        handleShowRegisterModal={handleShowRegisterModal} 
        handleShowWelcomeModal={handleShowWelcomeModal}
      />

      <RegisterModal
        show={showRegister}
        handleClose={handleCloseRegister}
      />

      <WelcomeModal
        show={showWelcome}
        handleClose={handleCloseWelcomeModal}
      />

      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/calendar" element={<CalendarPage />} /> {/* Add this line for the calendar page */}
      </Routes>
    </Router>
  );
}

export default App;
