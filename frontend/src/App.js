import { BrowserRouter, Routes, Route, useLocation } from 'react-router-dom';
import { LandingPage } from "./Components/Pages/LandingPage";
import { ActivityView } from './Components/Pages/ActivityView';
import { Tracker } from './Components/Pages/Tracker';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/activities" element={<ActivityView />} />
        <Route path="/tracker/:id" element={<Tracker />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
