import React, { useState } from "react";
import './LoginForm.css';

const LoginForm = (props) => {

	const[login, provideLogin]= useState('');
	const[password, providePassword]= useState('');
	
	const handleSubmit = (event) =>{
		event.preventDefault();

		props.onSubmit({
			login,
			password,
		});
	}

	return (
		<form className="form" onSubmit={handleSubmit}>
			<h1>Login</h1>
			<label htmlFor="name">Name</label>
			<input type="text" id="name" value={login} onChange={(e)=> provideLogin(e.target.value)}/>
			<label htmlFor="password">Password</label>
			<input type="password" id="password" value={password} onChange={(e)=> providePassword(e.target.value)}/>
			<button type="submit">Continue</button>
		</form>
	);
};

export default LoginForm;
