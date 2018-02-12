import React, { Component } from 'react';
import { connect } from 'react-redux'
import { Link } from 'react-router-dom';
import { customerActions } from '../_actions/customer.actions';

import { Loader } from '../_components/Common'
import CustomerList from '../_components/CustomerList'

import PropTypes from 'prop-types';

class HomePage extends Component {

    componentDidMount() {
        this.props.dispatch(customerActions.getCustomers(''));
    }

    render() {

        const { customersState } = this.props
        const { loading, customersData } = customersState

        return (
            <div>
                <nav aria-label="breadcrumb">
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item active" aria-current="page">Home</li>
                    </ol>
                </nav>
                <h2>Customer list</h2>
                
                <Link to="/customer/add" className="btn btn-secondary">
                    Add a customer
                </Link>

                {loading ? <Loader /> : (customersData.length > 0 &&
                    <CustomerList customersData={customersData} />)
                }

            </div>
        );
    }
}

HomePage.propTypes = {
    customersState: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
    const { customersState } = state;
    return {
        customersState
    }
}

export default connect(mapStateToProps)(HomePage)