import React from 'react';
import { Route } from 'react-router-dom';
import { connect } from 'react-redux';

import { alertActions } from '../_actions';
import { history } from '../_helpers';

import { Header, Footer } from '../_components/Common'
import HomePage from '../HomePage'
import CustomerEditPage from '../CustomerEditPage'
import CustomerDeletePage from '../CustomerDeletePage'
import CustomerAddPage from '../CustomerAddPage'

class App extends React.Component {
    constructor(props) {
        super(props);

        const { dispatch } = this.props;
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
                        <div class="row">
                            <div class="col-md-12">

                                {alert.message &&
                                    <div className={`alert ${alert.type}`} role="alert">
                                        {alert.message}
                                    </div>
                                }
                                <Route exact path="/" component={HomePage} />
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

function mapStateToProps(state) {
    const { alert } = state;
    return {
        alert
    };
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App }; 