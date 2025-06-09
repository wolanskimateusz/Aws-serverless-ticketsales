import { useState } from "react";
import EventList from "../../Components/EventList/EventList";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

function EventPage() {
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);

  const handleDateChange = (date: Date | null) => {
    setSelectedDate(date);
  };

  const handleResetDate = () => {
    setSelectedDate(null);  // Resetowanie daty
  };

  const formatDate = (date: Date | null): string => {
    if (!date) return "";
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const day = String(date.getDate()).padStart(2, "0");
    return `${year}-${month}-${day}`;
  };

  return (
    <div className="container py-4">
      <div className="row">
        <div className="col-md-9">
          <EventList searchQuery={selectedDate ? formatDate(selectedDate) : ""} />
        </div>

        {/* Kalendarz po prawej stronie */}
        <div className="col-md-3">
          <h5>Wybierz datę</h5>
          <DatePicker
            selected={selectedDate}
            onChange={handleDateChange}
            dateFormat="yyyy-MM-dd"
            className="form-control"
            placeholderText="Wybierz datę"
          />
          <button
            className="btn btn-secondary mt-3"
            onClick={handleResetDate}
          >
            Resetuj datę
          </button>
        </div>
        
      </div>
    </div>
  );
}

export default EventPage;
