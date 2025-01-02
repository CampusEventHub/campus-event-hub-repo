import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar';
import LoginModal from './components/LoginModal';
import RegisterModal from './components/RegisterModal'; // Import RegisterModal
import WelcomeModal from './components/WelcomeModal'; // Import WelcomeModal
import HomePage from './pages/HomePage';

function App() {
  const [showLogin, setShowLogin] = useState(false);
  const [showRegister, setShowRegister] = useState(false); // State for RegisterModal
  const [showWelcome, setShowWelcome] = useState(false); // State for WelcomeModal

  const handleShowLogin = () => setShowLogin(true);
  const handleCloseLogin = () => setShowLogin(false);
  
  const handleShowRegisterModal = () => setShowRegister(true); // Function to show RegisterModal
  const handleCloseRegister = () => setShowRegister(false); // Function to close RegisterModal
  
  const handleShowWelcomeModal = () => setShowWelcome(true); // Function to show WelcomeModal
  const handleCloseWelcomeModal = () => setShowWelcome(false); // Function to close WelcomeModal

  return (
    <Router>
      <NavBar onProfileClick={handleShowLogin} />
      
      {/* Pass handleShowWelcomeModal to LoginModal */}
      <LoginModal
        show={showLogin}
        handleClose={handleCloseLogin}
        handleShowRegisterModal={handleShowRegisterModal} 
        handleShowWelcomeModal={handleShowWelcomeModal} // Pass to LoginModal
      />

      {/* Register Modal */}
      <RegisterModal
        show={showRegister}
        handleClose={handleCloseRegister}
      />

      {/* Welcome Modal */}
      <WelcomeModal
        show={showWelcome}
        handleClose={handleCloseWelcomeModal}
      />

      <Routes>
        <Route path="/" element={<HomePage />} />
      </Routes>
    </Router>
  );
}

export default App;
