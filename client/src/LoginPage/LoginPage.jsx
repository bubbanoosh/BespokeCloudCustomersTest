import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { userActions } from '../_actions/user.actions';

import { LoginForm } from '../_components/_forms/LoginForm';

import PropTypes from 'prop-types';

class LoginPage extends Component {
    constructor(props) {
        super(props);

        // reset login status
        this.props.dispatch(userActions.logout());
    }

    render() {

        const Fragment = React.Fragment;
        const { userState } = this.props;
        const { loggingIn } = userState;

        return (
            <Fragment>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item active" aria-current="page">Login</li>
                    </ol>
                </nav>

                <div className="container">
                    <div className="row">
                        <div className="col-md-10">
                            <h2>Login</h2>
                        </div>
                        <div className="col-md-2">
                            <Link to="/register" className="btn btn-secondary">
                                Register...
                            </Link>
                        </div>
                    </div>
                    <div>

                    <LoginForm
                        loggingIn={loggingIn}
                        operationText={'Login to proceed'}
                        userAction={userActions.login}
                    />

                    </div>
                </div>

            </Fragment>
        );
    }
}

LoginPage.propTypes = {
    dispatch: PropTypes.func.isRequired,
    userState: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
    const { userState } = state;
    return {
        userState
    };
}

const connectedLoginPage = connect(mapStateToProps)(LoginPage);
export { connectedLoginPage as LoginPage }; 