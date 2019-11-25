import React from "react";
import { ILoginState } from "../../types/account";
import { login } from "../../services/account-services";

export default class Login extends React.Component<{}, ILoginState> {
    state = {
        username: '',
        password: ''
    }

    _login = async (e: any) => {
        e.preventDefault();
        var result = await login({ username: this.state.username, password: this.state.password });
        if (result.isSuccess) window.location.href = "/";
    }
    render() {
        return (
            <>
                <form onSubmit={this._login.bind(this)}>
                    <h1>Login page</h1>
                    <p>
                        <input value={this.state.username} onChange={e => this.setState({username: e.target.value})} type="text" placeholder="username" />
                    </p>
                    <p>
                        <input value={this.state.password} onChange={e => this.setState({password: e.target.value})} type="text" placeholder="username" />
                    </p>
                    <button>Submit</button>
                </form>
            </>
        )
    }
}