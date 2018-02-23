import React, { Component } from 'react';
import CustomerItem from './CustomerItem';
import PropTypes from 'prop-types';

class CustomerList extends Component {

    renderCustomers(customers) {
        if (customers.length > 0) {
            return customers.map(c => {
                return (
                    <CustomerItem
                        key={c.id}
                        customer={c}
                    />
                );
            });
        }
    }

    render() {

        const { customersData } = this.props;

        return (
            <ul className="list-group">
                {customersData.length > 0 && this.renderCustomers(customersData)}
            </ul>
        );
    }
}

CustomerList.propTypes = {
    customersData: PropTypes.object.isRequired,
};

export default CustomerList;