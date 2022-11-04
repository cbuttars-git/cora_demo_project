import { reactive } from "vue";
import { EventMessageUtils, EventType, InteractionStatus } from "@azure/msal-browser";
/**
 * Helper function to determine whether 2 arrays are equal
 * Used to avoid unnecessary state updates
 * @param arrayA
 * @param arrayB
 */
function accountArraysAreEqual(arrayA, arrayB) {
    if (arrayA.length !== arrayB.length) {
        return false;
    }
    const comparisonArray = [...arrayB];
    return arrayA.every((elementA) => {
        const elementB = comparisonArray.shift();
        if (!elementA || !elementB) {
            return false;
        }
        return (elementA.homeAccountId === elementB.homeAccountId) &&
            (elementA.localAccountId === elementB.localAccountId) &&
            (elementA.username === elementB.username);
    });
}
export const msalPlugin = {
    install: (app, msalInstance) => {
        console.log("In the MSAL Plugin");
        const inProgress = InteractionStatus.Startup;
        console.log("About to get all accounts");
        const accounts = msalInstance.getAllAccounts();
        console.log("Got My Accounts - length" + accounts.entries.length.toString());
        const state = reactive({
            instance: msalInstance,
            inProgress: inProgress,
            accounts: accounts
        });
        app.config.globalProperties.$msal = state;
        msalInstance.addEventCallback((message) => {
            console.log("Callback event type: " + message.eventType);
            console.log("Payload:", message.payload);
            switch (message.eventType) {
                case EventType.ACCOUNT_ADDED:
                case EventType.ACCOUNT_REMOVED:
                case EventType.LOGIN_SUCCESS:
                case EventType.SSO_SILENT_SUCCESS:
                case EventType.HANDLE_REDIRECT_END:
                case EventType.LOGIN_FAILURE:
                case EventType.SSO_SILENT_FAILURE:
                case EventType.LOGOUT_END:
                case EventType.ACQUIRE_TOKEN_SUCCESS:
                    //router.push("/");
                    break;
                case EventType.ACQUIRE_TOKEN_FAILURE: {
                    const currentAccounts = msalInstance.getAllAccounts();
                    if (!accountArraysAreEqual(currentAccounts, state.accounts)) {
                        state.accounts = currentAccounts;
                    }
                    break;
                }
            }
            const status = EventMessageUtils.getInteractionStatusFromEvent(message, state.inProgress);
            if (status !== null) {
                state.inProgress = status;
            }
        });
    }
};
//# sourceMappingURL=msalPlugin.js.map