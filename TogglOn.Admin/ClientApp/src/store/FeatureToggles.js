import { loadingType } from './Spinner';
import { addError } from './Error';

const requestFeatureTogglesType = 'REQUEST_FEATURETOGGLES';
const receiveFeatureTogglesType = 'RECEIVE_FEATURETOGGLES';
const requestUpdateFeatureToggleActivatedType = 'REQUEST_UPDATE_FEATURETOGGLE_ACTIVATED';
const receiveUpdateFeatureToggleActivatedType = 'RECEIVE_UPDATE_FEATURETOGGLE_ACTIVATED';
const initialState = [];

export const actionCreators = {
    requestFeatureToggles: () => async (dispatch) => {
        dispatch({ type: requestFeatureTogglesType });
        dispatch({ type: loadingType, active: true });

        const url = "api/featuretoggles";
        const response = await fetch(url);
        if(response.status === 200){
            const featureToggles = await response.json();

            dispatch({ type: receiveFeatureTogglesType, featureToggles });
        } else {
            dispatch(addError(response.statusText));
        }

        dispatch({ type: loadingType, active: false });
    },

    requestUpdateFeatureToggleActivated: (featureToggle) => async (dispatch) => {
        dispatch({ type: requestUpdateFeatureToggleActivatedType });
        dispatch({ type: loadingType, active: true });


        const id = featureToggle.id;
        const activated = !featureToggle.activated;

        const url = "api/featuretoggles/" + id;
        const response = await fetch(url, {
            method: 'PUT',
            body: JSON.stringify(activated),
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (response.status === 200) {
            dispatch({ type: receiveUpdateFeatureToggleActivatedType, id, activated });
        } else {
            dispatch(addError(response.statusText));
        }

        dispatch({ type: loadingType, active: false });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestFeatureTogglesType) {
        return state;
    }

    if (action.type === receiveFeatureTogglesType) {
        return [
            ...action.featureToggles
        ];
    }

    if (action.type === requestUpdateFeatureToggleActivatedType) {
        return state;
    }

    if (action.type === receiveUpdateFeatureToggleActivatedType) {
        const featureToggles = state;

        const index = featureToggles.findIndex(value => value.id === action.id);
        const featureToggle = featureToggles.splice(index, 1)[0];

        return [...featureToggles, { ...featureToggle, activated: action.activated }];
    }

    return state;
};
