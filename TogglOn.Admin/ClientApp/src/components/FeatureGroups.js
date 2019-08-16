import React, { Component } from 'react';

class FeatureGroups extends Component {
    componentWillMount() {
        this.props.requestFeatureGroups();
    }

    render() {
        return (
            <div>
                {renderFeatureGroups(this.props)}

            </div>
        );
    }
}

function renderFeatureGroups(props) {
    return (
        <table className='table'>
            <thead>
                <tr><th>Name</th></tr>
            </thead>
            <tbody>
                {props.featureGroups.map(group =>
                    <tr key={group.id}>
                        <td>{group.name}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

export default FeatureGroups;
