import React, { Component } from 'react'
import CustomerItem from './CustomerItem'

class CustomerList extends Component {

    renderCustomers(customers) {
        if (customers.length > 0) {
            return customers.map(c => {
                return (
                    <CustomerItem
                        key={c.id}
                        customer={c}
                    />
                )
            })
        }
    }

    render() {

        const { customersData } = this.props

        return (
            <ul className="list-group">
                {customersData.length > 0 && this.renderCustomers(customersData)}
            </ul>
        )
    }
}

export default CustomerList