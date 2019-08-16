import { connect } from 'react-redux';
import Error from './Error';

export default connect(
    state => {
        return { errors: state.errors };
    }
)(Error);
