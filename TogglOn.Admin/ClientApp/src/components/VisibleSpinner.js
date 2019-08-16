import { connect } from 'react-redux';
import Spinner from './Spinner';

export default connect(
    state => {
        return { spinner: state.spinner }
    }
)(Spinner);
