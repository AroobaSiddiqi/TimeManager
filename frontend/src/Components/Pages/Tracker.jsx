import React, { useState, useEffect, useRef } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { activities, colors } from '../data';


export const Tracker = () => {
    const [time, setTime] = useState(0);
    const [isRunning, setIsRunning] = useState(false);
    const intervalRef = useRef(null);
    const { id } = useParams();
    const navigate = useNavigate();
    const [activity, setActivity] = useState(null);
    const location = useLocation();
    const colorIndex = location.state?.color || 0;


    
    useEffect(() => {
        if (isRunning) {
            intervalRef.current = setInterval(() => {
                setTime((prevTime) => prevTime + 10);
            }, 10);
        } else {
            clearInterval(intervalRef.current);
        }

        return () => clearInterval(intervalRef.current);
    }, [isRunning]);

    useEffect(() => {
        const fetchedActivity = activities.find(activity => activity.id === parseInt(id));
        setActivity(fetchedActivity);
    }, [id]);



    const formatTime = (ms) => {
        const seconds = Math.floor((ms / 1000) % 60).toString().padStart(2, '0');
        const hours = Math.floor((ms / (1000 * 60 * 60)) % 60).toString().padStart(2, '0');

        return `${hours}:${seconds}`;
    };

    const handleToggle = () => {
        setIsRunning(!isRunning);
    };

    const handleEndSession = () => {
        setIsRunning(false);
        setTime(0);
    };

    return (
        <div className={`flex flex-col  ${colors[colorIndex]} h-screen`}>
        <div className="py-8 px-16">
            {activity ? (
                <>
                    <h2 className="text-2xl font-semibold mb-4">{activity.title}</h2>
                    <p className="activity-description">{activity.description}</p>
                </>
            ) : (
                <p>Loading activity...</p>
            )}
        </div>

        <div className="px-16 bg-white rounded-t-3xl shadow-[0_-5px_15px_rgba(0,0,0,0.1)] flex-grow">
 
            <div className="w-full pt-8 ">
                <h2 className="text-2xl font-semibold mb-4">Timer</h2>
                <div className="text-6xl font-bold mb-4">{formatTime(time)}</div>
                <button
                    className="bg-blue-500 text-white px-4 py-2 mr-2 rounded w-32"
                    onClick={handleToggle}
                >
                    {isRunning ? 'Pause' : 'Start'}
                </button>
                <button
                    className="bg-red-500 text-white px-4 py-2 mr-2 rounded w-32"
                    onClick={handleEndSession}
                >
                    End Session
                </button>
            </div>
            <div className="flex flex-col flex-grow pt-8">
                <h2 className="text-2xl font-semibold mb-4">Notes</h2>
                <textarea
                    className="flex-1 flex-grow p-4 border rounded resize-none"
                // value={notes}
                // onChange={(e) => setNotes(e.target.value)}
                />
            </div>
        </div>
    </div>

    );
};
