import React, { useState } from "react";
import "./LoginAttemptList.css";

const LoginAttempt = (props) => <li {...props}>{props.children}</li>;

const LoginAttemptList = (props) => {
    const [filter, setFilter] = useState('');

    const handleFilterChange = (event) => {
        setFilter(event.target.value);
    };

    const filteredAttempts = props.attempts.filter(attempt =>
        attempt.username.toLowerCase().includes(filter.toLowerCase())
    );

    return (
        <div className="Attempt-List-Main">
            <p>Recent activity</p>
            <input type="input" placeholder="Filter..." value={filter} onChange={handleFilterChange} />
            <ul className="Attempt-List">
                {filteredAttempts.map((attempt, index) => (
                    <LoginAttempt key={index}>{attempt.username}</LoginAttempt>
                ))}
            </ul>
        </div>
    );
};

export default LoginAttemptList;