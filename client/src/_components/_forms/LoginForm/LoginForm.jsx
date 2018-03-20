import React, { Component } from 'react';
import { Loader } from '../../../_components/Common';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';

class LoginForm extends Component {

    constructor(props) {
        super(props);

        this.state = {
            loggingIn: this.props.loggingIn,
            operationText: this.props.operationText,
            username: '',
            pwd: '',
            submitted: false
        };
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        this.setState({ [name]: value });
    };

    handleSubmit = (event) => {
        event.preventDefault();

        this.setState({ submitted: true });
        const { username, pwd } = this.state;
        const { dispatch } = this.props;
        if (username && pwd) {
            dispatch(this.props.login(username, pwd));
        }
    };

    render() {

        const Fragment = React.Fragment;
        const { loggingIn, operationText } = this.state;
        return (
            <Fragment>

                {loggingIn ?
                    <Loader /> :
                    <form name="form" onSubmit={this.handleSubmit}>
                        <div className="form-row">
                            <div className="form-group col-md-6">
                                <label htmlFor="emailAddress">Email</label>
                                <input type="email"
                                    className="form-control"
                                    name="username"
                                    id="username"
                                    placeholder="name@domain.com"
                                    value={this.state.username}
                                    onChange={this.handleChange}
                                />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="pwd">Password</label>
                                <input type="password"
                                    className="form-control"
                                    name="pwd"
                                    id="pwd"
                                    placeholder="Password"
                                    value={this.state.pwd}
                                    onChange={this.handleChange}
                                />
                            </div>
                        </div>
                        <div className="btn-group mr-2" role="group" aria-label="First group">
                            <button className="btn btn-primary">{operationText}</button>
                        </div>
                    </form>
                }
            </Fragment>
        );

    }
}

LoginForm.propTypes = {
    dispatch: PropTypes.func.isRequired,
    loggingIn: PropTypes.bool.isRequired,
    operationText: PropTypes.string.isRequired,
    login: PropTypes.func.isRequired,
};

LoginForm.defaultProps = {
    loggingIn: false,
};

const connectedLoginForm = connect()(LoginForm);
export { connectedLoginForm as LoginForm };