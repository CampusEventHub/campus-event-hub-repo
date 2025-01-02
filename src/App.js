import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar';
import LoginModal from './components/LoginModal';
import HomePage from './pages/HomePage';

function App() {
  const [showLogin, setShowLogin] = useState(false);

  const handleShowLogin = () => setShowLogin(true);
  const handleCloseLogin = () => setShowLogin(false);

  return (
    <Router>
      <NavBar onProfileClick={handleShowLogin} /> 
      <LoginModal show={showLogin} handleClose={handleCloseLogin} />
      
      <Routes>
        <Route path="/" element={<HomePage />} />
      </Routes>
    </Router>
  );
}

export default App;