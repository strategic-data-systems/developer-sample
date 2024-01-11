import React, { useState } from "react";
import './App.css';
import LoginForm from './LoginForm';
import LoginAttemptList from './LoginAttemptList';

const App = () => {
    const [loginAttempts, setLoginAttempts] = useState([]);
    const handleSubmit = ({ username, password }) => {
        setLoginAttempts(prevAttempts => [
            ...prevAttempts,
            { username: username, password: password }
        ]);
    };
    return (
        <div className="App">
            <LoginForm onSubmit={handleSubmit} />
            <LoginAttemptList attempts={loginAttempts} />
        </div>
    );
};

export default App;
