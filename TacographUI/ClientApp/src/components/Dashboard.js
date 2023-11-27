// src/components/Dashboard.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Dashboard = () => {
    const [violations, setViolations] = useState([]);
    const [filter, setFilter] = useState({});

    useEffect(() => {
        fetchData();
    }, [filter]); // Re-fetch data when the filter changes

    const fetchData = async () => {
        try {
            const result = await axios.get('https://localhost:5001/api/tachograph', { params: filter });
            setViolations(result.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    const handleFilterChange = (event) => {
        const { name, value } = event.target;
        setFilter({ ...filter, [name]: value });
    };

    return (
        <div>
            <h1>Tachograph Dashboard</h1>

            <label>
                Date:
                <input type="date" name="date" onChange={handleFilterChange} />
            </label>

            <label>
                Driver:
                <input type="text" name="driver" onChange={handleFilterChange} />
            </label>

            <h2>Single Drive Time Violations</h2>
            <ul>
                {violations.singleDriveTime && violations.singleDriveTime.map(driver => (
                    <li key={driver}>{driver}</li>
                ))}
            </ul>

            <h2>Rest Time Violations</h2>
            <ul>
                {violations.restTime && violations.restTime.map(driver => (
                    <li key={driver}>{driver}</li>
                ))}
            </ul>

            
        </div>
    );
};

export default Dashboard;
