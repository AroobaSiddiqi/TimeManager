import { useNavigate } from 'react-router-dom';
import { activities, colors } from '../data';
import React, { useState } from 'react';


export const ActivityView = () => {

  const navigate = useNavigate();
  const [showPopup, setShowPopup] = useState(false);


  const handleCardClick = (activity, colorIndex) => {
    if (!activity) {
      navigate('/add-project');
    } else {
      navigate(`/tracker/${activity.id}`,  { state: { color: colorIndex } });
    }
  };

  const handleAddActivity = () => {
    // Handle form submission here (e.g., add the new activity to your activities array)
    setShowPopup(false); // Close the popup after submission
  };


  return (
    <div className="p-8 sm:px-20 md:px-32 lg:px-52">
      <p className="text-xl font-bold mb-4">Select Activity</p>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
        <div
          className="rounded overflow-hidden cursor-pointer bg-white border border-gray-300"
          onClick={() => setShowPopup(true)}
        >
          <div className="p-6 flex flex-col items-center justify-center">
            <div className="font-semibold text-xl mb-2">Add New Activity</div>
            <p className="text-gray-700 text-base">Click here to add a new activity</p>
          </div>
        </div>

        {showPopup && (
          <div className="fixed inset-0 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded shadow-lg">
              {/* Add your form for creating a new activity here */}
              <button onClick={() => setShowPopup(false)}>Close</button>
              <button onClick={() => handleAddActivity()}>Add Activity</button>
            </div>
          </div>
        )}

        {activities.map((activity, index) => {
          const colorIndex = index % colors.length;
          return (
            <div
              key={activity.id}
              className={`rounded overflow-hidden cursor-pointer ${activity.isNew ? 'bg-white border border-gray-300' : 'shadow-lg ' + colors[colorIndex]}`}
              onClick={() => handleCardClick(activity, colorIndex)}
            >
              <div className={`p-6 ${activity.isNew ? 'flex flex-col items-center justify-center' : ''}`}>
                <div className="font-semibold text-xl mb-2">{activity.title}</div>
                <p className="text-gray-700 text-base line-clamp-3">{activity.description}</p>
              </div>
            </div>
          );
        })}
      </div>
    </div>

  );
};