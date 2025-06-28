import React, { useState } from "react";
import "./LoginAttemptList.css";

const LoginAttempt = ({ children }) => <li>{children}</li>;

const LoginAttemptList = ({ attempts }) => {
    const [filter, setFilter] = useState("");

    const filteredAttempts = attempts.filter((a) =>
        a.toLowerCase().includes(filter.toLowerCase())
    );

    return (
        <div className="Attempt-List-Main">
            <p>Recent activity</p>
            <input
                type="text"
                placeholder="Filter..."
                value={filter}
                onChange={(e) => setFilter(e.target.value)}
            />
            <ul className="Attempt-List">
                {filteredAttempts.map((a, i) => (
                    <LoginAttempt key={i}>{a}</LoginAttempt>
                ))}
            </ul>
        </div>
    );
};

export default LoginAttemptList;
