import { InteractionStatus, InteractionType } from "@azure/msal-browser";
import { ref, watch } from "vue";
import { useMsal } from "./useMsal";
export function useMsalAuthentication(interactionType, request) {
    const { instance, inProgress } = useMsal();
    const localInProgress = ref(false);
    const result = ref(null);
    const error = ref(null);
    const acquireToken = async (requestOverride) => {
        if (!localInProgress.value) {
            localInProgress.value = true;
            const tokenRequest = requestOverride || request;
            if (inProgress.value === InteractionStatus.Startup || inProgress.value === InteractionStatus.HandleRedirect) {
                try {
                    const response = await instance.handleRedirectPromise();
                    if (response) {
                        result.value = response;
                        error.value = null;
                        return;
                    }
                }
                catch (e) {
                    result.value = null;
                    error.value = e;
                    return;
                }
                ;
            }
            try {
                const response = await instance.acquireTokenSilent(tokenRequest);
                result.value = response;
                error.value = null;
            }
            catch (e) {
                if (inProgress.value !== InteractionStatus.None) {
                    return;
                }
                if (interactionType === InteractionType.Popup) {
                    instance.loginPopup(tokenRequest).then((response) => {
                        result.value = response;
                        error.value = null;
                    }).catch((e) => {
                        error.value = e;
                        result.value = null;
                    });
                }
                else if (interactionType === InteractionType.Redirect) {
                    await instance.loginRedirect(tokenRequest).catch((e) => {
                        error.value = e;
                        result.value = null;
                    });
                }
            }
            ;
            localInProgress.value = false;
        }
    };
    const stopWatcher = watch(inProgress, () => {
        if (!result && !error) {
            acquireToken();
        }
        else {
            stopWatcher();
        }
    });
    acquireToken();
    return {
        acquireToken,
        result,
        error,
        inProgress: localInProgress
    };
}
//# sourceMappingURL=useMsalAuthentication.js.map