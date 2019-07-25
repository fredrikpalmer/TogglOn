const requestFeatureTogglesType = 'REQUEST_FEATURETOGGLES';
const receiveFeatureTogglesType = 'RECEIVE_FEATURETOGGLES';
const requestUpdateFeatureToggleActivatedType = 'REQUEST_UPDATE_FEATURETOGGLE_ACTIVATED';
const receiveUpdateFeatureToggleActivatedType = 'RECEIVE_UPDATE_FEATURETOGGLE_ACTIVATED';
const initialState = { featureToggles: [], isLoading: false };

export const actionCreators = {
    requestFeatureToggles: () => async (dispatch) => {
        dispatch({ type: requestFeatureTogglesType });

        const url = "api/featuretoggles";
        const response = await fetch(url);
        const featureToggles = await response.json();

        dispatch({ type: receiveFeatureTogglesType, featureToggles });
    },

    requestUpdateFeatureToggleActivated: (featureToggle) => async (dispatch) => {
        dispatch({ type: requestUpdateFeatureToggleActivatedType });

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

        }
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestFeatureTogglesType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveFeatureTogglesType) {
        return {
            ...state,
            featureToggles: action.featureToggles,
            isLoading: false
        };
    }

    if (action.type === requestUpdateFeatureToggleActivatedType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveUpdateFeatureToggleActivatedType) {
        const featureToggles = state.featureToggles;

        const index = featureToggles.findIndex(value => value.id === action.id);
        const featureToggle = featureToggles.splice(index, 1)[0];

        return {
            ...state,
            featureToggles: [...featureToggles, { ...featureToggle, activated: action.activated }],
            isLoading: false
        };
    }

    return state;
};
