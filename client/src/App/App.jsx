import React from 'react';
import { Route } from 'react-router-dom';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';

import { alertActions } from '../_actions';
import { history } from '../_helpers';

import { PrivateRoute } from '../_components/PrivateRoute';
import { Header, Footer } from '../_components/Common';
import { HomePage } from '../HomePage';
import { LoginPage } from '../LoginPage';
import { CustomerEditPage } from '../CustomerEditPage';
import { CustomerDeletePage } from '../CustomerDeletePage';
import { CustomerAddPage } from '../CustomerAddPage';

class App extends React.Component {
    constructor(props) {
        super(props);

        const { dispatch } = this.props;
        // eslint-disable-next-line no-unused-vars
        history.listen((location, action) => {
            // clear alert on location change
            dispatch(alertActions.clear());
        });
    }

    render() {
        const { alert } = this.props;
        return (

            <div className="container">
                <Header />
                <main>
                    <div className="container">
                        <div className="row">
                            <div className="col-md-12">

                                {alert.message &&
                                    <div className={`alert ${alert.type}`} role="alert">
                                        {alert.message}
                                    </div>
                                }
                                <PrivateRoute exact path="/" component={HomePage} />
                                <Route path="/login" component={LoginPage} />
                                <Route exact path="/customer/add" component={CustomerAddPage} />
                                <Route exact path="/customer/edit/:id" component={CustomerEditPage} />
                                <Route exact path="/customer/delete/:id" component={CustomerDeletePage} />
                            </div>
                        </div>
                    </div>
                </main>
                <Footer />
            </div>
        );
    }
}

App.propTypes = {
    dispatch: PropTypes.func.isRequired,
    alert: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
    const { alert } = state;
    return {
        alert
    };
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App }; 