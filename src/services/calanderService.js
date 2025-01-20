const API_URL = 'https://localhost:7149/api/Kalendar/month/'; // Backend API base URL

export const fetchCalendarData = async (month, year) => {
  try {
    const url = `${API_URL}${month}/${year}`;
    console.log(`Fetching data from: ${url}`);
    
    const response = await fetch(url);
    
    if (!response.ok) {
      throw new Error('Failed to fetch events');
    }
    
    const data = await response.json();
    
    // Convert 'datum' to a Date object
    const eventsWithParsedDates = data.map(event => {
      const [day, month, year] = event.datum.split('.');
      const parsedDate = new Date(`${year}-${month}-${day}T${event.vrijeme}:00`);
      return {
        ...event,
        startDate: parsedDate,  // Store the parsed date here
      };
    });
    
    return eventsWithParsedDates;
  } catch (error) {
    console.error('Error fetching calendar data:', error);
    return [];
  }
};

