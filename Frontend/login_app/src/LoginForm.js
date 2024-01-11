import React, { useState } from "react";
import './LoginForm.css';

const LoginForm = (props) => {
    const [currentUsername, setCurrentUsername] = useState('');
    const [currentUserpassword, setCurrentUserPassword] = useState('');

    const handleCurrentUsernameChange = (event) => {
        setCurrentUsername(event.target.value);
    };

    const handleCurrentUserpasswordChange = (event) => {
        setCurrentUserPassword(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        props.onSubmit({
            username: currentUsername,
            password: currentUserpassword,
        });

    }

    return (
        <form className="form">
            <h1>Login</h1>
            <label htmlFor="name">Name</label>
            <input type="text" id="name" value={currentUsername} onChange={handleCurrentUsernameChange} />
            <label htmlFor="password">Password</label>
            <input type="password" id="password" value={currentUserpassword} onChange={handleCurrentUserpasswordChange}/>
            <button type="submit" onClick={handleSubmit}>Continue</button>
        </form>
    )
}

export default LoginForm;