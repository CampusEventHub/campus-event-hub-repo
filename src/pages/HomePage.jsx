import React, { useState, useEffect } from 'react';
import EventList from '../components/EventList';
import SearchNavbar from '../components/Navbar';
import { fetchEvents } from '../services/eventsService';

function HomePage() {
  const [events, setEvents] = useState([]);
  const [filteredEvents, setFilteredEvents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getEvents = async () => {
      try {
        const eventsData = await fetchEvents();
        setEvents(eventsData);
        setFilteredEvents(eventsData);
      } catch (error) {
        setError("Failed to load events");
      } finally {
        setLoading(false);
      }
    };

    getEvents();
  }, []);

  const handleSearch = (query) => {
    const searchQuery = query.toLowerCase();
    const filtered = events.filter(event =>
      event.title.toLowerCase().includes(searchQuery) ||
      (event.description && event.description.toLowerCase().includes(searchQuery))
    );
    setFilteredEvents(filtered);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div>
      <SearchNavbar onSearch={handleSearch} />
      <div className="event-header">
      <h2><b>EVENTS</b></h2>
      </div>
      <EventList events={filteredEvents} />
    </div>
  );
}

export default HomePage;