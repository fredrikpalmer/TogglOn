import { loadingType } from './Spinner';
import { addError } from './Error';

const requestFeatureGroupsType = 'REQUEST_FEATUREGROUPS';
const receiveFeatureGroupsType = 'RECEIVE_FEATUREGROUPS';
const initialState = [];

export const actionCreators = {
    requestFeatureGroups: () => async (dispatch) => {
        dispatch({ type: requestFeatureGroupsType });
        dispatch({ type: loadingType, active: true });

        const url = "api/featuregroups";
        const response = await fetch(url);
        if(response.status === 200){
            const featureGroups = await response.json();

            dispatch({ type: receiveFeatureGroupsType, featureGroups });
        }else{
            dispatch(addError(response.statusText));
        }

        dispatch({ type: loadingType, active: false });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestFeatureGroupsType) {
        return state;
    }

    if (action.type === receiveFeatureGroupsType) {
        return [
            ...action.featureGroups
        ];
    }

    return state;
};
