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

    // onCalculateAverageWeightClick = () => {
    //     this.props.calculateAverage(this.props.currentProducts);
    // }

    render() {

        const { customers } = this.props

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

                {customers.loading ? <Loader /> : (customers.customersData.length > 0 &&
                    <CustomerList customersData={customers.customersData} />)
                }

            </div>
        );
    }
}

HomePage.propTypes = {
    customers: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
    const { customers } = state;
    return {
        customers
    }
}

export default connect(mapStateToProps)(HomePage)