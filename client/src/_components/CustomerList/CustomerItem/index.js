import React from 'react'
import { Link } from 'react-router-dom';

const CustomerItem = props => {
    return (
        <li className="list-group-item">
            <div className="btn-group mr-2" role="group" aria-label="First group">
                <Link to={`/customer/edit/${props.customer.id}`} className="btn btn-outline-primary">
                    Edit
                </Link>
                <Link to={`/customer/delete/${props.customer.id}`} className="btn btn-outline-danger">
                    Delete
                </Link>
            </div>
            {props.customer.firstName} {props.customer.lastName}
        </li>
    )
}

export default CustomerItem