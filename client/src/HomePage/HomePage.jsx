import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { customerActions } from '../_actions/customer.actions';

import { Loader } from '../_components/Common';
import CustomerList from '../_components/CustomerList/CustomerList';
import SearchForm from '../_components/SearchForm/SearchForm';

import PropTypes from 'prop-types';

class HomePage extends Component {

    componentDidMount() {
        this.props.dispatch(customerActions.getCustomers(''));
    }

    render() {

        const Fragment = React.Fragment;
        const { customersState } = this.props;
        const { loading, customersData } = customersState;

        return (
            <Fragment>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item active" aria-current="page">Home</li>
                    </ol>
                </nav>

                <div className="container">
                    <div className="row">
                        <div className="col-md-10">
                            <h2>Customer list</h2>
                        </div>
                        <div className="col-md-2">
                            <Link to="/customer/add" className="btn btn-secondary">
                                Add a customer
                            </Link>
                        </div>
                    </div>
                </div>

                <SearchForm customerAction={customerActions.getCustomers} />

                {loading ? <Loader /> : (customersData.length > 0 &&
                    <CustomerList customersData={customersData} />)
                }

            </Fragment>
        );
    }
}

HomePage.propTypes = {
    dispatch: PropTypes.func.isRequired,
    customersState: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
    const { customersState } = state;
    return {
        customersState
    };
}

const connectedHomePage = connect(mapStateToProps)(HomePage);
export { connectedHomePage as HomePage }; 